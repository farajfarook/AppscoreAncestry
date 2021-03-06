import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { SearchComponent } from './components/search/search.component';
import { ResultGridComponent } from './components/result-grid/result-grid.component';
import { PeopleService } from './services/people.service';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        SearchComponent,
        ResultGridComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: '**', redirectTo: 'home' }
        ]),
    ],
    providers: [ PeopleService ],
})
export class AppModuleShared {
}
