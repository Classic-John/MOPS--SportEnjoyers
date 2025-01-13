import { Component } from '@angular/core';
import { Thread } from '../../../../../shared/interfaces/threads/thread.interface';
import { Group } from '../../../../../shared/interfaces/groups/group.interface';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ThreadService } from '../../../../../shared/services/thread/thread.service';
import { MessageService } from '../../../../../shared/services/message/message.service';
import { GroupService } from '../../../../../shared/services/group/group.service';
import { CreateMessage } from '../../../../../shared/interfaces/messages/create-message.interface';
import { Message } from '../../../../../shared/interfaces/messages/message.interface';

@Component({
  selector: 'app-thread',
  templateUrl: './thread.component.html',
  styleUrl: './thread.component.css'
})
export class ThreadComponent {
  thread: Thread | null = null;
  group: Group | null = null;
  messageForm!: FormGroup;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly threadService: ThreadService,
    private readonly messageService: MessageService,
    groupService: GroupService,
    formBuilder: FormBuilder
  ) {
    this.route.paramMap.subscribe({
      next: (params) => {
        let id = Number(params.get('id'));
        console.log(id);

        this.messageForm = formBuilder.group({
          threadId: id,
          text: ["", Validators.compose([Validators.required, Validators.maxLength(1000)])]
        });

        threadService.get(id).subscribe({
          next: (thread: Thread) => {
            this.thread = thread;

            groupService.get(thread.groupId).subscribe({
              next: (group: Group) => {
                this.group = group;
              },
              error: (err) => {
                console.log("Error: ", err);
              }
            });
          },
          error: (err) => {
            console.log("Error: ", err);
          }
        });
      }
    });
  }

  createMessage(message: CreateMessage) {
    this.messageService.create(message).subscribe({
      next: (message: Message) => {
        this.thread?.messages.push(message);
        console.log("Successfully added message!");
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }

  deleteThread() {
    if (this.thread == null) {
      return;
    }

    this.threadService.delete(this.thread.id).subscribe({
      next: () => {
        console.log("Thread deleted successfully!");
        this.router.navigate([".."], { relativeTo: this.route });
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }

  deleteMessage(messageId: Number) {
    this.messageService.delete(messageId).subscribe({
      next: () => {
        if (this.thread != null) {
          this.thread.messages = this.thread.messages.filter((message, _index, _array) => message.id != messageId);
          console.log("Successfully deleted message!");
        }
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }
}
