import { NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Note } from '../note';
import { NoteService } from '../note.service';

@Component({
  selector: 'app-note-edit',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './note-edit.component.html',
  styleUrl: './note-edit.component.scss'
})
export class NoteEditComponent implements OnInit {
  note?: Note;

  constructor(
    private noteService: NoteService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getNote();
  }

  getNote(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.noteService.getNote$(id).subscribe(note => this.note = note);
    }
  }

  saveChanges() {
    // Update the note
    this.noteService.updateNote$(this.note!).subscribe(() => this.router.navigateByUrl(''));
  }
}
