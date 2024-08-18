import { NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';

import { Note } from '../note';
import { NoteService } from '../note.service';

@Component({
  selector: 'app-note-edit',
  standalone: true,
  imports: [FormsModule, NgIf, NgbTooltipModule],
  templateUrl: './note-edit.component.html',
  styleUrl: './note-edit.component.scss'
})
export class NoteEditComponent implements OnInit {
  noteTitle = '';
  noteBody = '';
  note!: Note;

  constructor(
    private noteService: NoteService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  get archiveTooltip(): string {
    return this.note.isArchived ? 'Unarchive' : 'Archive';
  }
  
  get archiveIconClass(): string {
    return `bi bi-file-earmark-arrow-${this.note.isArchived ? 'up' : 'down'}`;
  }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe(({ note }) => {
      this.note = note;
      this.noteTitle = note.title;
      this.noteBody = note.body;
    });
  }

  async save() {
    // Save the changes to the note (if any)
    if (this.noteTitle !== this.note.title || this.noteBody !== this.note.body) {
      await this.saveInternal();
    }

    this.navigateHome();
  }

  async archive() {
    // Archive (or unarchive, depending on its current status) the note
    this.note.isArchived = !this.note.isArchived;

    // Save the changes
    await this.saveInternal();

    this.navigateHome();
  }

  navigateHome() {
    this.router.navigateByUrl('/notes');
  }

  private async saveInternal() {
    // Update the note and save the changes
    this.note.title = this.noteTitle;
    this.note.body = this.noteBody;
    await this.noteService.updateNoteAsync(this.note);
  }
}
