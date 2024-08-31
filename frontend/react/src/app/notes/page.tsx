// Page must be a Client Component to use the 'useState()' and 'useEffect()' hooks
'use client';

import { useEffect, useState } from "react";
import { Note } from "@/app/lib/note";
import { getNotesAsync } from "@/app/lib/note-service";
import NoteGrid from "@/app/ui/notes/note-grid";

export default function Page() {
  const [notes, setNotes] = useState(([] as Note[]));

  useEffect(() => {
    getNotesAsync().then((data) => setNotes(data));
  });

  return <NoteGrid notes={notes}/>;
}
