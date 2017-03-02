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
            noteContent = "New Note";
            noteTitle = "Note Title";
            noteContentForeground = Brushes.LightGray;
            showNotesListGrid();
            notesModel.loadData();
            notesModelSync();
            notesGridWidth = fullWidth / 2;
            notesCollapseButtonText = "Hide Quick Notes";
        }

        private Model.NotesModel notesModel = new Model.NotesModel();
        public ObservableCollection<Model.Note> notes { get; set; } = new ObservableCollection<Model.Note>();

        public void notesModelSync()
        {
            notes = new ObservableCollection<Model.Note>(notesModel.Notes);
            RaisePropertyChanged("notes");
        }

        #region NotesContent
        private string _noteContent;
        public string noteContent
        {
            get { return _noteContent; }
            set
            {
                if (value == _noteContent) return;
                _noteContent = value;
                RaisePropertyChanged("noteContent");
            }
        }

        private string _noteTitle;
        public string noteTitle
        {
            get { return _noteTitle; }
            set
            {
                if (value == _noteTitle) return;
                _noteTitle = value;
                RaisePropertyChanged("noteTitle");
            }
        }

        private string _noteEditTitle;
        public string noteEditTitle
        {
            get { return _noteEditTitle; }
            set
            {
                if (value == _noteEditTitle) return;
                _noteEditTitle = value;
                RaisePropertyChanged("noteEditTitle");
            }
        }

        private string _noteEditContent;
        public string noteEditContent
        {
            get { return _noteEditContent; }
            set
            {
                if (value == _noteEditContent) return;
                _noteEditContent = value;
                RaisePropertyChanged("noteEditContent");
            }
        }

        private string _noteTitleBeforeEdit;
        public string noteTitleBeforeEdit
        {
            get { return _noteTitleBeforeEdit; }
            set
            {
                if (value == _noteTitleBeforeEdit) return;
                _noteTitleBeforeEdit = value;
            }
        }
        
        private string _noteContentBeforeEdit;
        public string noteContentBeforeEdit
        {
            get { return _noteContentBeforeEdit; }
            set
            {
                if (value == _noteContentBeforeEdit) return;
                _noteContentBeforeEdit = value;
            }
        }
#endregion

        #region NotesVisual
        private void addNoteFormLostFocus()
        {
            if (noteContent == "") noteContent = "New Note";
            noteContentForeground = Brushes.LightGray;
        }

        private void noteTitleFormLostFocus()
        {
            if (noteTitle == "") noteTitle = "Note Title";
        }

        private void editNoteFormLostFocus()
        {
            if (noteEditContent == "") noteEditContent = "New Note";
            noteContentForeground = Brushes.LightGray;
        }

        private void editNoteTitleFormLostFocus()
        {
            if (noteEditTitle == "") noteEditTitle = "Note Title";
        }

        private void activateAddNoteForm()
        {
            if (noteContent == "New Note")
                noteContent = "";
            noteContentForeground = Brushes.White;
        }

        private void activateNoteTitleForm()
        {
            if (noteTitle == "Note Title")
                noteTitle = "";
        }

        private void activateEditNoteForm()
        {
            if (noteEditContent == "New Note")
                noteEditContent = "";
            noteContentForeground = Brushes.White;
        }

        private void activateEditNoteTitleForm()
        {
            if (noteEditTitle == "Note Title")
                noteEditTitle = "";
        }

        private void resetAddNoteForm()
        {
            noteContent = "New Note";
            noteContentForeground = Brushes.LightGray;
            noteTitle = "Note Title";
        }

        private void showNotesListGrid()
        {
            noteListGridVisibility = Visibility.Visible;
            noteAddGridVisibility = Visibility.Collapsed;
            noteEditGridVisibility = Visibility.Collapsed;
        }

        private void showAddNoteGrid()
        {
            noteListGridVisibility = Visibility.Collapsed;
            noteAddGridVisibility = Visibility.Visible;
        }

        private void showEditNoteGrid()
        {
            noteEditGridVisibility = Visibility.Visible;
            noteListGridVisibility = Visibility.Collapsed;
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

        private Visibility _noteAddGridVisibility;
        public Visibility noteAddGridVisibility
        {
            get { return _noteAddGridVisibility; }
            set
            {
                if (_noteAddGridVisibility == value) return;
                _noteAddGridVisibility = value;
                RaisePropertyChanged("noteAddGridVisibility");
            }
        }

        private Visibility _noteListGridVisibility;
        public Visibility noteListGridVisibility
        {
            get { return _noteListGridVisibility; }
            set
            {
                if (_noteListGridVisibility == value) return;
                _noteListGridVisibility = value;
                RaisePropertyChanged("noteListGridVisibility");
            }
        }

        private Visibility _noteEditGridVisibility;
        public Visibility noteEditGridVisibility
        {
            get { return _noteEditGridVisibility; }
            set
            {
                if (_noteEditGridVisibility == value) return;
                _noteEditGridVisibility = value;
                RaisePropertyChanged("noteEditGridVisibility");
            }
        }

        private Visibility _notesContentVisibility;
        public Visibility notesContentVisibility
        {
            get { return _notesContentVisibility; }
            set
            {
                if (value == _notesContentVisibility) return;
                _notesContentVisibility = value;
                RaisePropertyChanged("notesContentVisibility");
            }
        }

        private string _notesCollapseButtonText;
        public string notesCollapseButtonText
        {
            get { return _notesCollapseButtonText; }
            set
            {
                if (value == _notesCollapseButtonText) return;
                _notesCollapseButtonText = value;
                RaisePropertyChanged("notesCollapseButtonText");
            }
        }

        private double _notesGridWidth;
        public double notesGridWidth
        {
            get { return _notesGridWidth; }
            set
            {
                if (value == _notesGridWidth) return;
                _notesGridWidth = value;
                RaisePropertyChanged("notesGridWidth");
            }
        }
        #endregion

        #region commands
        private ICommand _NoteContentLeftButtonDown;
        public ICommand NoteContentLeftButtonDown
        {
            get
            {
                if(_NoteContentLeftButtonDown == null)
                {
                    _NoteContentLeftButtonDown = new RelayCommand(
                        o =>
                        {
                            activateAddNoteForm();
                            activateEditNoteForm();
                        });
                }
                return _NoteContentLeftButtonDown;
            }
        }

        private ICommand _NoteContentLostFocus;
        public ICommand NoteContentLostFocus
        {
            get
            {
                if(_NoteContentLostFocus == null)
                {
                    _NoteContentLostFocus = new RelayCommand(
                        o =>
                        {
                            addNoteFormLostFocus();
                            editNoteFormLostFocus();
                        });
                }
                return _NoteContentLostFocus;
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
                            activateNoteTitleForm();
                            activateEditNoteTitleForm();
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
                            noteTitleFormLostFocus();
                            editNoteTitleFormLostFocus();
                        });
                }
                return _NoteTitleLostFocus;
            }
        }

        private ICommand _NoteSaveNewNote;
        public ICommand NoteSaveNewNote
        {
            get
            {
                if(_NoteSaveNewNote == null)
                {
                    _NoteSaveNewNote = new RelayCommand(
                        o =>
                        {
                            if (noteContent != "")
                            {
                                notesModel.addItem(noteTitle, noteContent);
                                notesModelSync();
                                resetAddNoteForm();
                                showNotesListGrid();
                            }
                        });
                }
                return _NoteSaveNewNote;
            }
        }

        private ICommand _NoteAddShowGrid;
        public ICommand NoteAddShowGrid
        {
            get
            {
                if(_NoteAddShowGrid == null)
                {
                    _NoteAddShowGrid = new RelayCommand(
                        o =>
                        {
                            showAddNoteGrid();
                            resetAddNoteForm();
                        });
                }
                return _NoteAddShowGrid;
            }
        }

        private ICommand _NoteDelete;
        public ICommand NoteDelete
        {
            get
            {
                if(_NoteDelete == null)
                {
                    _NoteDelete = new RelayCommand(
                        o =>
                        {
                            Model.Note note = o as Model.Note;
                            notesModel.removeItem(note);
                            notesModelSync();
                        });
                }
                return _NoteDelete;
            }
        }

        private ICommand _NoteCancel;
        public ICommand NoteCancel
        {
            get
            {
                if(_NoteCancel == null)
                {
                    _NoteCancel = new RelayCommand(
                        o =>
                        {
                            showNotesListGrid();
                            resetAddNoteForm();
                        });
                }
                return _NoteCancel;
            }
        }

        private ICommand _NoteEditShowGrid;
        public ICommand NoteEditShowGrid
        {
            get
            {
                if(_NoteEditShowGrid == null)
                {
                    _NoteEditShowGrid = new RelayCommand(
                        o =>
                        {
                            Model.Note note = o as Model.Note;
                            showEditNoteGrid();
                            noteEditContent = note.content;
                            noteEditTitle = note.title;
                            noteContentBeforeEdit = note.content;
                            noteTitleBeforeEdit = note.title;
                        });
                }
                return _NoteEditShowGrid;
            }
        }

        private ICommand _NoteEditSave;
        public ICommand NoteEditSave
        {
            get
            {
                if(_NoteEditSave == null)
                {
                    _NoteEditSave = new RelayCommand(
                        o =>
                        {
                            notesModel.editItem(noteTitleBeforeEdit, noteContentBeforeEdit, noteEditTitle, noteEditContent);
                            notesModelSync();
                            showNotesListGrid();
                        });
                }
                return _NoteEditSave;
            }
        }

        private ICommand _NotesSaveData;
        public ICommand NotesSaveData
        {
            get
            {
                if(_NotesSaveData == null)
                {
                    _NotesSaveData = new RelayCommand(
                        o =>
                        {
                            notesModel.saveData();
                        });
                }
                return _NotesSaveData;
            }
        }

        private ICommand _NotesGridCollapse;
        public ICommand NotesGridCollapseExtend
        {
            get
            {
                if (_NotesGridCollapse == null)
                    _NotesGridCollapse = new RelayCommand(
                        o =>
                        {
                            if (notesContentVisibility == Visibility.Visible)
                            {
                                width = width - notesGridWidth + 25;
                                notesGridWidth = 25;
                                notesCollapseButtonText = "Extend Quick Notes";
                                notesContentVisibility = Visibility.Collapsed;
                            }
                            else
                            {
                                notesGridWidth = fullWidth / 2;
                                width = width + notesGridWidth - 25;
                                notesCollapseButtonText = "Hide Quick Notes";
                                notesContentVisibility = Visibility.Visible;
                            }
                        });
                return _NotesGridCollapse;
            }
        }
        #endregion

        #endregion
    }
}
