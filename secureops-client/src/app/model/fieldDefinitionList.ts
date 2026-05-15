
import { FieldType } from "./fieldType";

export interface FieldDefinitionList {
    id: number,
    label: string,
    options: string;
    fieldType: FieldType
    displayOrder: number
}