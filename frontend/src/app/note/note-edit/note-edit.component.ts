import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { Note } from '../note';

@Component({
  selector: 'app-note-edit',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './note-edit.component.html',
  styleUrl: './note-edit.component.scss'
})
export class NoteEditComponent {
  noteTitle = '';
  noteBody = '';
  @Output() createNoteEvent = new EventEmitter<Note>();

  createNote() {
    // Notify subscriber(s) of the new note to create
    const note: Note = {
      id: '',
      title: this.noteTitle,
      body: this.noteBody
    };
    this.createNoteEvent.emit(note);

    // Clear the input forms
    this.noteTitle = '';
    this.noteBody = '';
  }
}
