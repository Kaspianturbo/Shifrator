using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShifratorWPF
{
    public class MainPresenter
    {
        private readonly IMainForm _view;
        private readonly IFileManager _manager;
        private readonly IMessageService _messageService;
        private readonly ICoder _coder;

        private string _currentFilePath;

        public MainPresenter(IMainForm view, IFileManager manager, IMessageService service, ICoder coder)
        {
            _view = view;
            _manager = manager;
            _messageService = service;
            _coder = coder;

            _view.SetSymbolCount(0);

            _view.ContentChanged += _view_ContentChanged;
            _view.FileOpenClick += _view_FileOpenClick;
            _view.FileSaveClick += _view_FileSaveClick;
            _view.DecipherClick += _view_DecipherClick;
            _view.EncrypterClick += _view_EncrypterClick;
            _view.FontChanged += _view_FontChanged;
        }

        private void _view_FontChanged(object sender, EventArgs e)
        {
            try
            {
                _view.GetFontSize();
            }
            catch (Exception)
            {
                _messageService.ShowError("Введіть числове значення");
            }
        }

        private void _view_EncrypterClick(object sender, EventArgs e)
        {
            try
            {
                _coder.CreateKey(_view.Content, _coder.GetKeyPath(_view.FilePath));
                _coder.UpDateText(_view.Content, _view.FilePath);
            }
            catch (Exception)
            {
                _messageService.ShowError("Відкрийте потрібний файл");
            }

        }

        private void _view_DecipherClick(object sender, EventArgs e)
        {
            try
            {
                _coder.Decipher(_view.Content, _view.KeyPath, _view.FilePath);
            }
            catch (Exception)
            {
                _messageService.ShowError("Відкрийте потрібний файл");
            }
        }

        private void _view_FileSaveClick(object sender, EventArgs e)
        {
            try
            {
                string content = _view.Content;

                _manager.SaveContent(content, _currentFilePath);

                _messageService.ShowMessage("Файл успішно збережено");
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }

        private void _view_FileOpenClick(object sender, EventArgs e)
        {
            try
            {
                string filePath = _view.FilePath;

                bool isExist = _manager.IsExist(filePath);

                if (!isExist)
                {
                    _messageService.ShowExclamation("Вибраний файл не існує.");
                    return;
                }

                _currentFilePath = filePath;

                string content = _manager.GetContent(filePath);
                int count = _manager.GetSymbolCount(content);

                _view.Content = content;
                _view.SetSymbolCount(count);
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }

        }

        private void _view_ContentChanged(object sender, EventArgs e)
        {
            string content = _view.Content;

            int count = _manager.GetSymbolCount(content);

            _view.SetSymbolCount(count);
        }
    }
}
