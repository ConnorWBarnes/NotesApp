// Page must be a Client Component to use the 'useState()' and 'useEffect()' hooks
'use client';

import { useState, useEffect } from 'react';

import { Note } from "@/app/lib/note";
import { getNoteAsync } from "@/app/lib/note-service";
import NoteEditForm from "@/app/ui/notes/note-edit-form";

export default function Page({ params }: { params: { noteId: string } }) {
  const [note, setNote] = useState(({} as Note));

  useEffect(() => {
    getNoteAsync(params.noteId).then((data) => setNote(data));
  });

  return <NoteEditForm note={note}/>;
}
