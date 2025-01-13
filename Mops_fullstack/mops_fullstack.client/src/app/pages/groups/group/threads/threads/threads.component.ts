import { Component } from '@angular/core';
import { Thread } from '../../../../../shared/interfaces/threads/thread.interface';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GroupService } from '../../../../../shared/services/group/group.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateThread } from '../../../../../shared/interfaces/threads/create-thread.interface';
import { ThreadService } from '../../../../../shared/services/thread/thread.service';
import { ThreadSummary } from '../../../../../shared/interfaces/threads/thread-summary.interface';
import { Group } from '../../../../../shared/interfaces/groups/group.interface';

@Component({
  selector: 'app-threads',
  templateUrl: './threads.component.html',
  styleUrl: './threads.component.css'
})
export class ThreadsComponent {
  threadList: Thread[] = [];
  threadForm!: FormGroup;

  constructor(
    private readonly route: ActivatedRoute,
    groupService: GroupService,
    private readonly threadService: ThreadService,
    private readonly router: Router,
    formBuilder: FormBuilder
  ) {
    route.paramMap.subscribe({
      next: (params) => {
        let id = Number(params.get('id'));

        this.threadForm = formBuilder.group({
          groupId: id,
          initialMessage: ["", Validators.compose([Validators.required, Validators.maxLength(1000)])]
        });

        groupService.getThreads(id).subscribe({
          next: (threads) => {
            this.threadList = threads;
          },
          error: (err) => {
            console.log("Error: ", err);
          }
        });
      }
    });
  }

  createThread(thread: CreateThread) {
    this.threadService.create(thread).subscribe({
      next: (thread: ThreadSummary) => {
        this.router.navigate([`${thread.id}`], { relativeTo: this.route });
        console.log("Thread successfully created!");
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }
}
