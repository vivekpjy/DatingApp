import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { UserParams } from 'src/app/_models/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css'],
})
export class MemberListComponent implements OnInit {
  members: Member[] = [];
  pagination: Pagination | undefined;
  userparams: UserParams | undefined;
  genderList = [
    { value: 'male', title: 'Male' },
    { value: 'female', title: 'Female' },
  ];

  constructor(private memberService: MembersService) {
    this.userparams = this.memberService.getUserParams();
  }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    if (this.userparams) {
      this.memberService.setUserParams(this.userparams);
      this.memberService.getMembers(this.userparams).subscribe({
        next: (response) => {
          if (response.pagination && response.result) {
            this.members = response.result;
            this.pagination = response.pagination;
          }
        },
      });
    }
  }

  resetFilters() {
    this.userparams = this.memberService.resetUserParams();
    this.loadMembers();
  }

  pageChanged(event: any) {
    if (this.userparams && this.userparams?.pageNumber !== event.page) {
      this.userparams.pageNumber = event.page;
      this.memberService.setUserParams(this.userparams);
      this.loadMembers();
    }
  }
}
