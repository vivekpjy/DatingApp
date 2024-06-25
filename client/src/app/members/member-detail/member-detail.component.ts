import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TimeagoModule } from 'ngx-timeago';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
  imports: [CommonModule, TabsModule, GalleryModule, TimeagoModule],
})
export class MemberDetailComponent implements OnInit {
  member: Member | undefined;
  image: GalleryItem[] = [];

  constructor(
    private memberService: MembersService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    const user = this.route.snapshot.paramMap.get('username');
    if (!user) return;
    this.memberService.getMember(user).subscribe({
      next: (res) => {
        (this.member = res), this.getImages();
      },
    });
  }

  getImages() {
    if (!this.member) return;
    for (const photo of this.member.photos) {
      this.image.push(new ImageItem({ src: photo.url, thumb: photo.url }));
    }
  }
}
