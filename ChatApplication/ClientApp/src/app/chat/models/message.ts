import { ObjectId } from "mongodb";

export class Message {
    SenderId: string;
    RecipientId: string;
    Type: string;
    SentOn: Date;
    Content: string;
}
