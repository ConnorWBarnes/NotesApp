import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { Note } from '../note';
import { NoteService } from '../note.service';

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

  constructor(
    private noteService: NoteService,
    private router: Router
  ) { }

  async createNote() {
    // Create the note
    const note: Note = {
      id: '',
      title: this.noteTitle,
      body: this.noteBody,
      isArchived: false
    };

    // Save the note to the server
    await this.noteService.createNoteAsync(note);

    // Navigate home
    this.navigateHome();
  }
  
  navigateHome() {
    this.router.navigateByUrl('/notes');
  }
}
