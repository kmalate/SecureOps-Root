export interface IncidentMetadata {
    fieldDefinitionId: number; // Links to your FieldDefinition entity
    value: string;             // Stores the answer (e.g., "True", "Vehicle Theft", "High")
}