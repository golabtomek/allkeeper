using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Allkeeper.ViewModel
{
    public class Notes : INotifyPropertyChanged
    {
        public ObservableCollection<Model.Note> notes { get; set; } = new ObservableCollection<Model.Note>();
        private Model.Notes model = new Model.Notes();

        private string _noteTitle;
        public string noteTitle
        {
            get { return _noteTitle; }
            set
            {
                if (_noteTitle == value) return;
                _noteTitle = value;
                RaisePropertyChanged("noteTitle");
            }
        }

        private string _noteContent;
        public string noteContent
        {
            get { return _noteContent; }
            set
            {
                if (_noteContent == value) return;
                _noteContent = value;
                RaisePropertyChanged("noteContent");
            }
        }

        private string _noteTitleBeforeEdit;
        public string noteTitleBeforeEdit
        {
            get { return _noteTitleBeforeEdit; }
            set
            {
                if (_noteTitleBeforeEdit == value) return;
                _noteTitleBeforeEdit = value;
                RaisePropertyChanged("noteTitleBeforeEdit");
            }
        }

        private string _noteContentBeforeEdit;
        public string noteContentBeforeEdit
        {
            get { return _noteContentBeforeEdit; }
            set
            {
                if (_noteContentBeforeEdit == value) return;
                _noteContentBeforeEdit = value;
                RaisePropertyChanged("noteContentBeforeEdit");
            }
        }

        private void modelSync()
        {
            notes = new ObservableCollection<Model.Note>(model.notes);
            RaisePropertyChanged("notes");
        }

        public Notes()
        {
            showMainGrid();
            resetNote();
            model.loadData();
            modelSync();
        }

        private Visibility _mainGridVisibility;
        public Visibility mainGridVisibility
        {
            get { return _mainGridVisibility; }
            set
            {
                if (_mainGridVisibility == value) return;
                _mainGridVisibility = value;
                RaisePropertyChanged("mainGridVisibility");
            }
        }

        private Visibility _addGridVisibility;
        public Visibility addGridVisibility
        {
            get { return _addGridVisibility; }
            set
            {
                if (_addGridVisibility == value) return;
                _addGridVisibility = value;
                RaisePropertyChanged("addGridVisibility");
            }
        }

        private Visibility _editGridVisibility;
        public Visibility editGridVisibility
        {
            get { return _editGridVisibility; }
            set
            {
                if (_editGridVisibility == value) return;
                _editGridVisibility = value;
                RaisePropertyChanged("editGridVisibility");
            }
        }

        private Brush _noteTitleForeground;
        public Brush noteTitleForeground
        {
            get { return _noteTitleForeground; }
            set
            {
                if (value == _noteTitleForeground) return;
                _noteTitleForeground = value;
                RaisePropertyChanged("noteTitleForeground");
            }
        }

        private Brush _noteContentForeground;
        public Brush noteContentForeground
        {
            get { return _noteContentForeground; }
            set
            {
                if (value == _noteContentForeground) return;
                _noteContentForeground = value;
                RaisePropertyChanged("noteContentForeground");
            }
        }

        private void showAddGrid()
        {
            addGridVisibility = Visibility.Visible;
            mainGridVisibility = Visibility.Collapsed;
            editGridVisibility = Visibility.Collapsed;
        }

        private void showEditGrid()
        {
            editGridVisibility = Visibility.Visible;
            mainGridVisibility = Visibility.Collapsed;
            addGridVisibility = Visibility.Collapsed;
        }

        private void showMainGrid()
        {
            mainGridVisibility = Visibility.Visible;
            editGridVisibility = Visibility.Collapsed;
            addGridVisibility = Visibility.Collapsed;
        }

        private void resetNote()
        {
            noteTitle = "Note Title";
            noteContent = "New Note";
        }


        #region Commands
        private ICommand _saveNewNote;
        public ICommand saveNewNote
        {
            get
            {
                if(_saveNewNote == null)
                {
                    _saveNewNote = new RelayCommand(
                        o =>
                        {
                            model.addItem(noteTitle, noteContent);
                            modelSync();
                            showMainGrid();
                            resetNote();
                        });
                }
                return _saveNewNote;
            }
        }

        private ICommand _saveEditNote;
        public ICommand saveEditNote
        {
            get
            {
                if(_saveEditNote == null)
                {
                    _saveEditNote = new RelayCommand(
                        o =>
                        {
                            model.editItem(noteTitleBeforeEdit, noteContentBeforeEdit, noteTitle, noteContent);
                            modelSync();
                            showMainGrid();
                            resetNote();
                        });
                }
                return _saveEditNote;
            }
        }

        private ICommand _removeNote;
        public ICommand removeNote
        {
            get
            {
                if(_removeNote == null)
                {
                    _removeNote = new RelayCommand(
                        o =>
                        {
                            Model.Note note = o as Model.Note;
                            model.removeItem(note.title, note.content);
                            modelSync();
                        });
                }
                return _removeNote;
            }
        }

        private ICommand _cancelAddOrEditNote;
        public ICommand cancelAddOrEditNote
        {
            get
            {
                if (_cancelAddOrEditNote == null)
                {
                    _cancelAddOrEditNote = new RelayCommand(
                        o =>
                        {
                            showMainGrid();
                            resetNote();
                        });
                }
                return _cancelAddOrEditNote;
            }
        }
        
        private ICommand _showAddForm;
        public ICommand showAddForm
        {
            get
            {
                if (_showAddForm == null)
                {
                    _showAddForm = new RelayCommand(
                        o =>
                        {
                            showAddGrid();
                        });
                }
                return _showAddForm;
            }
        }

        private ICommand _showEditForm;
        public ICommand showEditForm
        {
            get
            {
                if (_showEditForm == null)
                {
                    _showEditForm = new RelayCommand(
                        o =>
                        {
                            Model.Note note = o as Model.Note;
                            noteTitle = note.title;
                            noteContent = note.content;
                            noteContentBeforeEdit = note.content;
                            noteTitleBeforeEdit = note.title;
                            showEditGrid();
                        });
                }
                return _showEditForm;
            }
        }

        private ICommand _contentGotFocus;
        public ICommand contentGotFocus
        {
            get
            {
                if(_contentGotFocus == null)
                {
                    _contentGotFocus = new RelayCommand(
                        o =>
                        {
                            if (noteContent == "New Note")
                                noteContent = "";
                        });
                }
                return _contentGotFocus;
            }
        }

        private ICommand _titleGotFocus;
        public ICommand titleGotFocus
        {
            get
            {
                if (_titleGotFocus == null)
                {
                    _titleGotFocus = new RelayCommand(
                        o =>
                        {
                            if (noteTitle == "Note Title")
                                noteTitle = "";
                        });
                }
                return _titleGotFocus;
            }
        }

        private ICommand _contentLostFocus;
        public ICommand contentLostFocus
        {
            get
            {
                if (_contentLostFocus == null)
                {
                    _contentLostFocus = new RelayCommand(
                        o =>
                        {
                            if (noteContent == "")
                                noteContent = "New Note";
                        });
                }
                return _contentLostFocus;
            }
        }

        private ICommand _titleLostFocus;
        public ICommand titleLostFocus
        {
            get
            {
                if (_titleLostFocus == null)
                {
                    _titleLostFocus = new RelayCommand(
                        o =>
                        {
                            if (noteTitle == "")
                                noteTitle = "Note Title";
                        });
                }
                return _titleLostFocus;
            }
        }

        private ICommand _saveNotesData;
        public ICommand saveNotesData
        {
            get
            {
                if(_saveNotesData == null)
                {
                    _saveNotesData = new RelayCommand(
                        o =>
                        {
                            model.saveData();
                        });
                }
                return _saveNotesData;
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        
    }
}
