import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User } from '../_models';
import { config } from "../config";
import { Video } from '../_models/video';
import { Observable } from 'rxjs';
import { VideoUploadResponse } from '../_models/videoUpload';

@Injectable({ providedIn: 'root' })
export class VideoService {
    private videoList: Video[]
    constructor(private http: HttpClient) { }

    async getAll():Promise<void> {
        this.videoList = await this.http.get<Video[]>(`${config.springUrl}/all`).toPromise()
    }

    save(video: Video) {
        return this.http.post<Video>(`${config.springUrl}`, video);
    }

    saveVideoFile(file: File) {
        const formData = new FormData();
        formData.append("video", file, file.name);
        return this.http.post(`${config.videoServiceUrl}/upload`, formData);
    }

    delete(id: number) {
        return this.http.delete(`${config.springUrl}/${id}`);
    }
    public get getVideoList():Array<Video> { return this.videoList; }
}
