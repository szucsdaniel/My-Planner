import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { DragScrollListDirective } from './drag-scroll-list.directive';
import { BranchDetailComponent } from './branch-detail/branch-detail.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ProjectFormComponent } from './project-form/project-form.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ProjectDetailComponent,
    DragScrollListDirective,
    BranchDetailComponent,
    ProjectFormComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
