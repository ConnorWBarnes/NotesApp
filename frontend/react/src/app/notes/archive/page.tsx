// Page must be a Client Component to use the 'useState()' and 'useEffect()' hooks
'use client';

import { useState, useEffect } from 'react';

import { Note } from "@/app/lib/note";
import { getArchivedNotesAsync } from "@/app/lib/note-service";
import NoteGrid from "@/app/ui/notes/note-grid";

export default function Page() {
  const [notes, setNotes] = useState(([] as Note[]));

  useEffect(() => {
    getArchivedNotesAsync().then((data) => setNotes(data));
  });

  return <NoteGrid notes={notes}/>;
}
