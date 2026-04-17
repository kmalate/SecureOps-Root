import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { MainLayout } from './components/layout/main-layout/main-layout';
import { authGuard } from './guards/auth.guard';
import { guestGuard } from './guards/guest.guard';

export const routes: Routes = [
    {
        path: 'login',
        component: Login,
        title: 'Login',
        canActivate: [guestGuard]

    },
    // Secure App Routes (Wrapped in MainLayout)
    {
        path: '',
        component: MainLayout,
        canActivate: [authGuard],
        //TODO: Add child routes for dashboard, incidents, etc.
        // children: [
        //     { path: 'dashboard', component: DashboardComponent },
        //     { path: 'incidents', component: IncidentListComponent },
        //     { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
        // ]
    },

    // Wildcard
    { path: '**', redirectTo: 'login' }
];
