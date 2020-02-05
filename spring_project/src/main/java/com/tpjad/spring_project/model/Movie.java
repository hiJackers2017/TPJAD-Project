package com.tpjad.spring_project.model;

import javax.persistence.Entity;
import java.util.Date;

@Entity
public class Movie extends BaseEntity {
    private String title;
    private Date createdAt;
    private Integer createdBy;
    private String genre;
    private String videoFile;

    public String getVideoFile() {
        return videoFile;
    }

    public void setVideoFile(String videoFile) {
        this.videoFile = videoFile;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public void setCreatedAt(Date createdAt) {
        this.createdAt = createdAt;
    }

    public void setCreatedBy(Integer createdBy) {
        this.createdBy = createdBy;
    }

    public void setGenre(String genre) {
        this.genre = genre;
    }

    public String getTitle() {
        return title;
    }

    public Date getCreatedAt() {
        return createdAt;
    }

    public Integer getCreatedBy() {
        return createdBy;
    }

    public String getGenre() {
        return genre;
    }
}