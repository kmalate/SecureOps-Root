
import { Employee } from "./employee";
import { IncidentCategory } from "./incidentCategory";
import { IncidentSeverity } from "./incidentSeverity";
import { IncidentStatus } from "./incidentStatus";

export interface IncidentListItem {
    id: string;
    category: IncidentCategory;
    severity: IncidentSeverity;
    occuredAt: Date;
    createdBy: Employee;
    reportedBy: Employee;
    createdAt: Date;
    status: IncidentStatus;
}