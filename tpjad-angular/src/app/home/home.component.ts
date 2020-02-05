import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { User } from '../_models';
import { UserService, AuthenticationService } from '../_services';
import { VideoService } from '../_services/video.service';
import { Video } from '../_models/video';
import { config } from '../config';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { VideoUploadResponse } from '../_models/videoUpload';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit {
    currentUser: User;
    isCreateTab = true;
    currentVideo = new Video(0,"","","","","");
    videoForm: FormGroup;

    constructor(
        private formBuilder: FormBuilder,
        private authenticationService: AuthenticationService,
        private userService: UserService,
        private videoService: VideoService,
    ) {
    }

    ngOnInit() {
        this.videoService.getAll();
        this.videoForm = this.formBuilder.group({
            title: ['', Validators.required],
            genre: ['', Validators.required],
            videoFile: [null, Validators.required],
        });
    }

    changeTab() {
        console.log(this.isCreateTab);
        this.isCreateTab = !this.isCreateTab;
    }

    selectVideo(video: Video) {
        this.currentVideo = video;
    }

    deleteVideo(id:number) {
        this.videoService.delete(id).subscribe(() => this.videoService.getAll());
    }

    onSubmit() {
        if (this.videoForm.invalid) {
            return;
        }
        
        this.videoService.saveVideoFile(this.videoForm.value.videoFile)
            .subscribe(response => {
                console.log(response);
                const formData = this.videoForm.value;
                const video = new Video(null, formData.title,response["videoFileName"],formData.genre,null,null);
                console.log(video);
                this.videoService.save(video)
                    .subscribe(_ => {
                        this.videoService.getAll();
                        this.isCreateTab = false;
                    });
            });

    }
    onFileChange(files: FileList) {
        this.videoForm.patchValue({
            videoFile: files.item(0)
        });
          
      }

    get videoUrl():string {
        return `${config.videoServiceUrl}/stream?video=${this.currentVideo.videoFile}`;
    }

    get videoList():Video[] {
        return this.videoService.getVideoList;
    }
}
