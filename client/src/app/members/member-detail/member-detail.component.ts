import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { TabDirective, TabsModule, TabsetComponent } from 'ngx-bootstrap/tabs';
import { TimeagoModule } from 'ngx-timeago';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { MemberMessagesComponent } from '../member-messages/member-messages.component';
import { MessagesService } from 'src/app/_services/messages.service';
import { Message } from 'src/app/_models/message';
import { PresenceService } from 'src/app/_services/presence.service';
import { take } from 'rxjs';
import { AccountService } from 'src/app/_services/account.service';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
  imports: [
    CommonModule,
    TabsModule,
    GalleryModule,
    TimeagoModule,
    MemberMessagesComponent,
  ],
})
export class MemberDetailComponent implements OnInit, OnDestroy {
  @ViewChild('memberTabs', { static: true }) memberTabs?: TabsetComponent;
  member: Member = {} as Member;
  image: GalleryItem[] = [];
  activeTab?: TabDirective;
  messages: Message[] = [];
  user?: User;

  constructor(
    private accuntService: AccountService,
    private route: ActivatedRoute,
    private messageService: MessagesService,
    public presenceService: PresenceService
  ) {
    this.accuntService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        if (user) this.user = user;
      },
    });
  }

  ngOnInit(): void {
    // this.loadMember();
    this.route.data.subscribe({
      next: (data) => (this.member = data['member']),
    });

    this.route.queryParams.subscribe({
      next: (params) => {
        params['tab'] && this.selectTab(params['tab']);
      },
    });

    this.getImages();
  }

  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }

  selectTab(heading: string) {
    if (this.memberTabs) {
      this.memberTabs.tabs.find((x) => x.heading === heading)!.active = true;
    }
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    if (this.activeTab.heading === 'Messages' && this.user) {
      // this.loadMessages()
      this.messageService.createHubConnection(this.user, this.member.userName);
    } else {
      this.messageService.stopHubConnection();
    }
  }
  loadMessages() {
    if (this.member) {
      this.messageService.getMessageThread(this.member.userName).subscribe({
        next: (response) => (this.messages = response),
      });
    }
  }
  // loadMember() {
  //   const user = this.route.snapshot.paramMap.get('username');
  //   if (!user) return;
  //   this.memberService.getMember(user).subscribe({
  //     next: (res) => {
  //       (this.member = res), this.getImages();
  //     },
  //   });
  // }

  getImages() {
    if (!this.member) return;
    for (const photo of this.member.photos) {
      this.image.push(new ImageItem({ src: photo.url, thumb: photo.url }));
    }
  }
}
