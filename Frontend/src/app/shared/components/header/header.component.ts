import { Component, Input, OnInit, inject } from '@angular/core';
import { UserProfileService } from '../../data-access/user-profile/user-profile.service';
import { GetCurrentUsersProfileResponse } from '../../types/openapi';
import { ButtonModule } from 'primeng/button';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { CommonModule, Location } from '@angular/common';
import { TooltipModule } from 'primeng/tooltip';

@Component({
  selector: 'playlist-cleaner-header',
  standalone: true,
  imports: [CommonModule, ButtonModule, TooltipModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {

  @Input() headerText: string = "";
  
  private readonly userProfileService = inject(UserProfileService);

  private subscription = new Subscription();

  profileData: GetCurrentUsersProfileResponse | null = null;

  constructor(private router: Router, private location: Location) { }

  async ngOnInit() {
    this.initialiseSubscriptions();
    this.userProfileService.getUserProfile();
  }

  private initialiseSubscriptions(){
    this.subscription.add(
      this.userProfileService.profileObject$.subscribe((res) => {
        this.profileData = res;        
      })
    )
  }

  navigateBackwards(){
    this.location.back();
  }

  navigateToWelcome() {
    this.router.navigate(['']);
  }

  navigateToSpotify(){
    window.location.href = this.profileData?.spotify_external_url!
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
