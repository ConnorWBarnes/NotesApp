import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { Note } from '../note';
import { NoteComposeBaseComponent } from '../note-compose-base/note-compose-base.component';

@Component({
  selector: 'app-note-create',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './../note-compose-base/note-compose-base.component.html',
  styleUrl: './../note-compose-base/note-compose-base.component.scss'
})
export class NoteCreateComponent extends NoteComposeBaseComponent {
  override saveButtonText: string = 'Create Note';
  @Output() createNoteEvent = new EventEmitter<Note>();

  override save(): void {
    // Notify subscriber(s) of the new note to create
    const note: Note = {
      id: '',
      title: this.noteTitle,
      body: this.noteBody,
      isArchived: false
    };
    this.createNoteEvent.emit(note);

    // Clear the input forms
    this.noteTitle = '';
    this.noteBody = '';
  }
}
