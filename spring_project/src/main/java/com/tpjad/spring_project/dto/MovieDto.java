package com.tpjad.spring_project.dto;

import java.util.Date;

public class MovieDto {
    private Long id;
    private String title;
    private Date createdAt;
    private UserDto createdBy;
    private String genre;
    private String videoFile;

    public MovieDto(Long id, String title, Date createdAt, UserDto createdBy, String genre, String videoFile) {
        this.id = id;
        this.title = title;
        this.createdAt = createdAt;
        this.createdBy = createdBy;
        this.genre = genre;
        this.videoFile = videoFile;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public Date getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(Date createdAt) {
        this.createdAt = createdAt;
    }

    public UserDto getCreatedBy() {
        return createdBy;
    }

    public void setCreatedBy(UserDto createdBy) {
        this.createdBy = createdBy;
    }

    public String getGenre() {
        return genre;
    }

    public void setGenre(String genre) {
        this.genre = genre;
    }

    public String getVideoFile() {
        return videoFile;
    }

    public void setVideoFile(String videoFile) {
        this.videoFile = videoFile;
    }
}
