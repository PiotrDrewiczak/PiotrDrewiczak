import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PolygonComponent } from './polygon/polygon.component';
import { HttpClientModule } from '@angular/common/http';
import { PolygonService } from './polygon/service/polygon.service';
import { FormsModule } from '@angular/forms';
import { TopbarComponent } from './topbar/topbar.component';

@NgModule({
  declarations: [
    AppComponent,
    PolygonComponent,
    TopbarComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule 
  ],
  providers: [PolygonService],
  bootstrap: [AppComponent]
})
export class AppModule { }
