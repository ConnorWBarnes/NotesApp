import { NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Note } from '../note';
import { NoteGridItemComponent } from '../note-grid-item/note-grid-item.component';

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
  styleUrl: './note-grid.component.scss'
})
export class NoteGridComponent implements OnInit {
  notes!: Note[];

  constructor(
    private activatedRoute: ActivatedRoute, 
  ) { }
  
  ngOnInit(): void {
    this.activatedRoute.data.subscribe(({ notes }) => {
      this.notes = notes;
    });
  }
}
