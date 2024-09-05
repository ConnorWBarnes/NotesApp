import { getNotesAsync } from "@/app/lib/note-service";
import NoteGrid from "@/app/ui/notes/note-grid";

export default async function Page() {
  let notes = await getNotesAsync();
  return <NoteGrid notes={notes}/>;
}
