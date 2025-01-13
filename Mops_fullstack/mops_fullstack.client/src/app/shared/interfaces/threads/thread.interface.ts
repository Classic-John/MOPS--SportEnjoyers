import { Message } from "../messages/message.interface";

export interface Thread {
  id: Number,
  groupId: Number,
  messages: Message[],
}
