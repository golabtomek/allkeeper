using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace allkeeper.ViewModel
{
    public partial class MainViewModel
    {
        #region NotesVM
        public void NotesConstructor()
        {
            NoteTextBoxText = "New Note";
            NoteTitleTextBox = "Note Title";
            NoteTextBoxForeground = Brushes.LightGray;
            ShowNotesListGrid();
            notesModel.loadData();
            NotesModelSync();
        }

        private Model.NotesModel notesModel = new Model.NotesModel();
        public ObservableCollection<Model.Note> NotesList { get; set; } = new ObservableCollection<Model.Note>();

        public void NotesModelSync()
        {
            NotesList = new ObservableCollection<Model.Note>(notesModel.NotesList);
            RaisePropertyChanged("NotesList");
        }

        #region TextBoxes
        private string _NoteTextBoxText;
        public string NoteTextBoxText
        {
            get { return _NoteTextBoxText; }
            set
            {
                if (value == _NoteTextBoxText) return;
                _NoteTextBoxText = value;
                RaisePropertyChanged("NoteTextBoxText");
            }
        }

        private string _NoteTitleTextBox;
        public string NoteTitleTextBox
        {
            get { return _NoteTitleTextBox; }
            set
            {
                if (value == _NoteTitleTextBox) return;
                _NoteTitleTextBox = value;
                RaisePropertyChanged("NoteTitleTextBox");
            }
        }

        private string _EditNoteTitleTextBox;
        public string EditNoteTitleTextBox
        {
            get { return _EditNoteTitleTextBox; }
            set
            {
                if (value == _EditNoteTitleTextBox) return;
                _EditNoteTitleTextBox = value;
                RaisePropertyChanged("EditNoteTitleTextBox");
            }
        }

        private string _BeforeEditNoteTitleTextBox;
        public string BeforeEditNoteTitleTextBox
        {
            get { return _BeforeEditNoteTitleTextBox; }
            set
            {
                if (value == _BeforeEditNoteTitleTextBox) return;
                _BeforeEditNoteTitleTextBox = value;
            }
        }

        private string _EditNoteTextBoxText;
        public string EditNoteTextBoxText
        {
            get { return _EditNoteTextBoxText; }
            set
            {
                if (value == _EditNoteTextBoxText) return;
                _EditNoteTextBoxText = value;
                RaisePropertyChanged("EditNoteTextBoxText");
            }
        }

        private string _BeforeEditNoteTextBoxText;
        public string BeforeEditNoteTextBoxText
        {
            get { return _BeforeEditNoteTextBoxText; }
            set
            {
                if (value == _BeforeEditNoteTextBoxText) return;
                _BeforeEditNoteTextBoxText = value;
            }
        }
#endregion
        #region NotesVisualProperties
        private void AddNoteFormLostFocus()
        {
            if (NoteTextBoxText == "") NoteTextBoxText = "New Note";
            NoteTextBoxForeground = Brushes.LightGray;
        }

        private void NoteTitleFormLostFocus()
        {
            if (NoteTitleTextBox == "") NoteTitleTextBox = "Note Title";
        }

        private void EditNoteFormLostFocus()
        {
            if (EditNoteTextBoxText == "") EditNoteTextBoxText = "New Note";
            NoteTextBoxForeground = Brushes.LightGray;
        }

        private void EditNoteTitleFormLostFocus()
        {
            if (EditNoteTitleTextBox == "") EditNoteTitleTextBox = "Note Title";
        }

        private void ResetAddNoteForm()
        {
            NoteTextBoxText = "New Note";
            NoteTextBoxForeground = Brushes.LightGray;
            NoteTitleTextBox = "Note Title";
        }

        private void ShowNotesListGrid()
        {
            NoteListGridVisibility = Visibility.Visible;
            AddNoteGridVisibility = Visibility.Collapsed;
            NoteEditGridVisibility = Visibility.Collapsed;
        }

        private void ShowAddNoteGrid()
        {
            NoteListGridVisibility = Visibility.Collapsed;
            AddNoteGridVisibility = Visibility.Visible;
        }

        private void ShowEditNoteGrid()
        {
            NoteEditGridVisibility = Visibility.Visible;
            NoteListGridVisibility = Visibility.Collapsed;
        }

        private void ActivateAddNoteForm()
        {
            if (NoteTextBoxText == "New Note")
                NoteTextBoxText = "";
            NoteTextBoxForeground = Brushes.White;
        }

        private void ActivateNoteTitleForm()
        {
            if (NoteTitleTextBox == "Note Title")
                NoteTitleTextBox = "";
        }

        private void ActivateEditNoteForm()
        {
            if (EditNoteTextBoxText == "New Note")
                NoteTextBoxText = "";
            NoteTextBoxForeground = Brushes.White;
        }

        private void ActivateEditNoteTitleForm()
        {
            if (BeforeEditNoteTitleTextBox == "Note Title")
                NoteTitleTextBox = "";
        }

        private Brush _NoteTextBoxForeground;
        public Brush NoteTextBoxForeground
        {
            get { return _NoteTextBoxForeground; }
            set
            {
                if (value == _NoteTextBoxForeground) return;
                _NoteTextBoxForeground = value;
                RaisePropertyChanged("NoteTextBoxForeground");
            }
        }

        private Visibility _AddNoteGridVisibility;
        public Visibility AddNoteGridVisibility
        {
            get { return _AddNoteGridVisibility; }
            set
            {
                if (_AddNoteGridVisibility == value) return;
                _AddNoteGridVisibility = value;
                RaisePropertyChanged("AddNoteGridVisibility");
            }
        }

        private Visibility _NoteListGridVisibility;
        public Visibility NoteListGridVisibility
        {
            get { return _NoteListGridVisibility; }
            set
            {
                if (_NoteListGridVisibility == value) return;
                _NoteListGridVisibility = value;
                RaisePropertyChanged("NoteListGridVisibility");
            }
        }

        private Visibility _NoteEditGridVisibility;
        public Visibility NoteEditGridVisibility
        {
            get { return _NoteEditGridVisibility; }
            set
            {
                if (_NoteEditGridVisibility == value) return;
                _NoteEditGridVisibility = value;
                RaisePropertyChanged("NoteEditGridVisibility");
            }
        }
        #endregion

        #region commands
        private ICommand _AddTextTextBoxLeftButtonDown;
        public ICommand AddTextTextBoxLeftButtonDown
        {
            get
            {
                if(_AddTextTextBoxLeftButtonDown == null)
                {
                    _AddTextTextBoxLeftButtonDown= new RelayCommand(
                        o =>
                        {
                            ActivateAddNoteForm();
                            ActivateEditNoteForm();
                        });
                }
                return _AddTextTextBoxLeftButtonDown;
            }
        }

        private ICommand _AddTextTextBoxLostFocus;
        public ICommand AddTextTextBoxLostFocus
        {
            get
            {
                if(_AddTextTextBoxLostFocus==null)
                {
                    _AddTextTextBoxLostFocus = new RelayCommand(
                        o =>
                        {
                            AddNoteFormLostFocus();
                            EditNoteFormLostFocus();
                        });
                }
                return _AddTextTextBoxLostFocus;
            }
        }

        private ICommand _NoteTitleLeftButtonDown;
        public ICommand NoteTitleLeftButtonDown
        {
            get
            {
                if (_NoteTitleLeftButtonDown == null)
                {
                    _NoteTitleLeftButtonDown = new RelayCommand(
                        o =>
                        {
                            ActivateNoteTitleForm();
                            ActivateEditNoteTitleForm();
                        });
                }
                return _NoteTitleLeftButtonDown;
            }
        }

        private ICommand _NoteTitleLostFocus;
        public ICommand NoteTitleLostFocus
        {
            get
            {
                if (_NoteTitleLostFocus == null)
                {
                    _NoteTitleLostFocus = new RelayCommand(
                        o =>
                        {
                            NoteTitleFormLostFocus();
                            EditNoteTitleFormLostFocus();
                        });
                }
                return _NoteTitleLostFocus;
            }
        }

        private ICommand _SaveNewNote;
        public ICommand SaveNewNote
        {
            get
            {
                if(_SaveNewNote == null)
                {
                    _SaveNewNote = new RelayCommand(
                        o =>
                        {
                            if (NoteTextBoxText != "" && NoteTextBoxText != "New Note")
                            {
                                notesModel.addItem(NoteTitleTextBox, NoteTextBoxText);
                                NotesModelSync();
                                ResetAddNoteForm();
                                ShowNotesListGrid();
                            }
                        });
                }
                return _SaveNewNote;
            }
        }

        private ICommand _AddNote;
        public ICommand AddNote
        {
            get
            {
                if(_AddNote == null)
                {
                    _AddNote = new RelayCommand(
                        o =>
                        {
                            ShowAddNoteGrid();
                            ResetAddNoteForm();
                        });
                }
                return _AddNote;
            }
        }

        private ICommand _DeleteNote;
        public ICommand DeleteNote
        {
            get
            {
                if(_DeleteNote == null)
                {
                    _DeleteNote = new RelayCommand(
                        o =>
                        {
                            Model.Note note = o as Model.Note;
                            if (NotesList.Contains(note))
                                notesModel.removeItem(note);
                            NotesModelSync();
                        });
                }
                return _DeleteNote;
            }
        }

        private ICommand _CancelAddNote;
        public ICommand CancelAddNote
        {
            get
            {
                if(_CancelAddNote == null)
                {
                    _CancelAddNote = new RelayCommand(
                        o =>
                        {
                            ShowNotesListGrid();
                            ResetAddNoteForm();
                        });
                }
                return _CancelAddNote;
            }
        }

        private ICommand _EditNote;
        public ICommand EditNote
        {
            get
            {
                if(_EditNote==null)
                {
                    _EditNote = new RelayCommand(
                        o =>
                        {
                            Model.Note note = o as Model.Note;
                            ShowEditNoteGrid();
                            EditNoteTextBoxText = note.Content;
                            EditNoteTitleTextBox = note.Title;
                            BeforeEditNoteTextBoxText = note.Content;
                            BeforeEditNoteTitleTextBox = note.Title;
                        });
                }
                return _EditNote;
            }
        }

        private ICommand _SaveEditNote;
        public ICommand SaveEditNote
        {
            get
            {
                if(_SaveEditNote == null)
                {
                    _SaveEditNote = new RelayCommand(
                        o =>
                        {
                            notesModel.editItem(BeforeEditNoteTitleTextBox, BeforeEditNoteTextBoxText, EditNoteTitleTextBox, EditNoteTextBoxText);
                            NotesModelSync();
                            ShowNotesListGrid();
                        });
                }
                return _SaveEditNote;
            }
        }

        private ICommand _SaveData;
        public ICommand SaveData
        {
            get
            {
                if(_SaveData==null)
                {
                    _SaveData = new RelayCommand(
                        o =>
                        {
                            notesModel.saveData();
                        });
                }
                return _SaveData;
            }
        }
        #endregion

        #endregion
    }
}
