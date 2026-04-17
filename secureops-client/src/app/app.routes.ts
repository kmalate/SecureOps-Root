import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { MainLayout } from './components/layout/main-layout/main-layout';

export const routes: Routes = [
    {
        path: 'login',
        component: Login,
        title: 'Login',
    },
    // Secure App Routes (Wrapped in MainLayout)
    {
        path: '',
        component: MainLayout,
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
