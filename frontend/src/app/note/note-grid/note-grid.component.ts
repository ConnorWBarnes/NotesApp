import { NgFor } from '@angular/common';
import { Component } from '@angular/core';

import { Note } from '../note';
import { NoteGridItemComponent } from '../note-grid-item/note-grid-item.component';
import { NoteService } from '../note.service';

/**
 * A collection of notes arranged in a grid-like layout.
 */
@Component({
  selector: 'app-note-grid',
  standalone: true,
  imports: [
    NgFor,
    NoteGridItemComponent
  ],
  templateUrl: './note-grid.component.html',
  styleUrl: './note-grid.component.css'
})
export class NoteGridComponent {
  notes!: Note[];

  /**
   * Initializes a new instance of the NoteGridComponent class.
   */
  constructor(private noteService: NoteService) {
    this.notes = this.noteService.getNotes();
  }
}
