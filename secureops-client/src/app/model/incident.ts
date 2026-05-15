import { IncidentMetadata } from "./incidentMetadata";
import { IncidentStatus } from "./incidentStatus";

export interface Incident {
    id: string,
    incidentCategoryId: number,
    incidentSeverityId: number,
    occurredAt: Date,
    createdById: number,
    narrative: string,
    reportedById: number,
    caseNumber: string,
    createdAt: Date,
    status: IncidentStatus
    metadata: IncidentMetadata;
    updatedById: number;
    updatedAt: Date;
}