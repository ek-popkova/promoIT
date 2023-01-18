export class DateHelper {

    public static dateToString(input: Date): string {
        const year: string = input.getFullYear().toString();
        const month: string = (input.getMonth() + 1).toString().padStart(2, "0");
        const day: string = (input.getDate()).toString().padStart(2, "0");
        const hour: string = (input.getHours()).toString().padStart(2, "0");
        const minute: string = (input.getMinutes()).toString().padStart(2, "0");
        const second: string = (input.getSeconds()).toString().padStart(2, "0");

        return `${year}${month}${day} ${hour}:${minute}:${second}`;
    }

    public static dateToStringforTwitter(input: Date): string {

        //YYYY-MM-DDTHH:mm:ssZ. The oldest UTC timestamp from which the Tweets will be provided. 
        //Timestamp is in second granularity and is inclusive(i.e. 12: 00: 01 includes the first second of the minute).
        const year: string = input.getFullYear().toString();
        const month: string = (input.getMonth() + 1).toString().padStart(2, "0");
        const day: string = (input.getDate()).toString().padStart(2, "0");
        const hour: string = (input.getHours()).toString().padStart(2, "0");
        const minute: string = (input.getMinutes()).toString().padStart(2, "0");
        const second: string = (input.getSeconds()).toString().padStart(2, "0");
//2022-12-28T12:00:00Z
        return `${year}-${month}-${day}T${hour}:${minute}:${second}Z`;
    }
}