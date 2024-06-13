import { Component, Input } from '@angular/core';

import { Note } from '../note';

/**
 * Displays a single note in the note grid.
 */
@Component({
  selector: 'app-note-grid-item',
  standalone: true,
  imports: [],
  templateUrl: './note-grid-item.component.html',
  styleUrl: './note-grid-item.component.scss'
})
export class NoteGridItemComponent {
  @Input() note!: Note;
}
