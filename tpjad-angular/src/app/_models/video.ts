import { User } from './user';

export class Video {
    constructor(
        public id: number,
        public title: string,
        public videoFile: string,
        public genre: string,
        public createdAt: string,
        public createdBy: User,) {}
}