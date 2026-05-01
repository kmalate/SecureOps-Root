import { FieldTarget } from "./fieldTarget";

export interface FieldDefinition {
    id: number,
    label: string,
    target: FieldTarget
    fieldTypeId: number,
    options: string;
    displayOrder: number
}