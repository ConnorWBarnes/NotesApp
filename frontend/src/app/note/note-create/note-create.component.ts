import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { Note } from '../note';

@Component({
  selector: 'app-note-create',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './note-create.component.html',
  styleUrl: './note-create.component.scss'
})
export class NoteCreateComponent {
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
