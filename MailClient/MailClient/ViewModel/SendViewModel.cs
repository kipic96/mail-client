using MailClient.Model.Entity;
using MailClient.ViewModel.Base;
using System;
using System.Windows.Input;

namespace MailClient.ViewModel
{
    class SendViewModel : BaseViewModel
    {
        private ICommand _sendCommand;
        private Mail _mail;

        public SendViewModel()
        {
            PageType = Enum.PageType.Send;
            PageName = Properties.Resources.Send;
        }

        public Mail Mail
        {
            get
            {
                if (_mail == null)
                    _mail = new Mail { };
                return _mail;
            }
            set
            {
                _mail.Id = value.Id;
                _mail.Subject = value.Subject;
                _mail.From = value.From;
                _mail.Message = value.Message;
                _mail.To = value.To;
                _mail.Attachment1 = value.Attachment1;
                _mail.Attachment2 = value.Attachment2;
                _mail.Attachment3 = value.Attachment1;
                RaisePropertyChanged(nameof(Mail));
            }
        }

        private string _firstFile = string.Empty;
        public string FirstFile
        {
            get
            {
                return _firstFile;
            }
            set
            {
                _firstFile = value;
                RaisePropertyChanged(nameof(FirstFile));
            }
        }

        private string _secondFile = string.Empty;
        public string SecondFile
        {
            get
            {
                return _secondFile;
            }
            set
            {
                _secondFile = value;
                RaisePropertyChanged(nameof(SecondFile));
            }
        }

        private string _thirdFile = string.Empty;
        public string ThirdFile
        {
            get
            {
                return _thirdFile;
            }
            set
            {
                _thirdFile = value;
                RaisePropertyChanged(nameof(ThirdFile));
            }
        }



        public ICommand SendCommand
        {
            get
            {
                if (_sendCommand == null)
                {
                    _sendCommand = new RelayCommand(p => Send());
                }
                return _sendCommand;                
            }
        }

        private ICommand _attachFirstFile;
        public ICommand AttachFirstFile
        {
            get
            {
                if (_attachFirstFile == null)
                {
                    _attachFirstFile = new RelayCommand(p => AttachFile(1));
                }
                return _attachFirstFile;
            }
        }

        private ICommand _attachSecondFile;
        public ICommand AttachSecondFile
        {
            get
            {
                if (_attachSecondFile == null)
                {
                    _attachSecondFile = new RelayCommand(p => AttachFile(2));
                }
                return _attachSecondFile;
            }
        }

        private ICommand _attachThirdFile;
        public ICommand AttachThirdFile
        {
            get
            {
                if (_attachThirdFile == null)
                {
                    _attachThirdFile = new RelayCommand(p => AttachFile(3));
                }
                return _attachThirdFile;
            }
        }

        public Action<Mail> SendMail { get; set; }

        public Func<string, bool> ValidateEmail { get; set; }

        private void Send()
        {
            if (!string.IsNullOrWhiteSpace(Mail.To) && 
                !string.IsNullOrWhiteSpace(Mail.Subject) && 
                !string.IsNullOrWhiteSpace(Mail.Message))
            {
                if (ValidateEmail(Mail.To))
                {
                    SendMail(Mail);
                    Mail = new Mail { };
                    FirstFile = string.Empty;
                    SecondFile = string.Empty;
                    ThirdFile = string.Empty;
                }
                else
                {
                    Log.LogMessage.Show(Properties.Resources.WrongEmailAdress);
                }
            }
            else
            {
                Log.LogMessage.Show(Properties.Resources.FillInToSubjectAndBody);
            }            
        }

        private void AttachFile(int attachmentNumber)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = "*.*";
            dlg.Filter = "All files (*.*)|*.*";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                if (attachmentNumber == 1)
                {
                    Mail.Attachment1.FileName = filename;
                    FirstFile = filename;
                }                    
                if (attachmentNumber == 2)
                {
                    Mail.Attachment2.FileName = filename;
                    SecondFile = filename;
                }                    
                if (attachmentNumber == 3)
                {
                    Mail.Attachment3.FileName = filename;
                    ThirdFile = filename;
                }
            }
        }
    }
}
