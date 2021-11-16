
import { NoteDto } from "src/app/services/service-proxy/service-proxy";

export class TodoItem extends NoteDto{
  constructor(noteText: string | undefined) {
    super();
    this.noteText = noteText
    this.isComplete = false
  }
}
