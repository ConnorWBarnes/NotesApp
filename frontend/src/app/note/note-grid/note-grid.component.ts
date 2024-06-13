import { NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';

import { Note } from '../note';
import { NoteEditComponent } from '../note-edit/note-edit.component';
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
    NoteEditComponent,
    NoteGridItemComponent
  ],
  templateUrl: './note-grid.component.html',
  styleUrl: './note-grid.component.scss'
})
export class NoteGridComponent implements OnInit {
  notes!: Note[];

  constructor(private noteService: NoteService) { }

  ngOnInit(): void {
    this.getNotes();
  }

  getNotes(): void {
    this.noteService.getNotes$().subscribe(notes => this.notes = notes);
  }

  createNote(note: Note) {
    this.noteService.createNote$(note).subscribe(createdNoteId => {
      note.id = createdNoteId;
      this.notes.push(note);
    });
  }
}
