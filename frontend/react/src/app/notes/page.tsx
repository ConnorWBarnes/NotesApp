import { getNotesAsync } from "@/lib/note-service";
import NoteGrid from "@/components/notes/note-grid";

export default async function Page() {
  let notes = await getNotesAsync();
  return <NoteGrid notes={notes}/>;
}
