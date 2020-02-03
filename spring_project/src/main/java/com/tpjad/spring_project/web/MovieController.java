package com.tpjad.spring_project.web;

import com.tpjad.spring_project.exceptions.RecordNotFoundException;
import com.tpjad.spring_project.model.Movie;
import com.tpjad.spring_project.service.MovieService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.Date;
import java.util.List;

@RestController
@RequestMapping("/movies")
public class MovieController
{
    @Autowired
    MovieService service;

    @GetMapping
    public ResponseEntity<List<Movie>> getAllMovies() {
        List<Movie> list = service.getAllMovies();

        return new ResponseEntity<List<Movie>>(list, new HttpHeaders(), HttpStatus.OK);
    }

    @GetMapping("/{id}")
    public ResponseEntity<Movie> getMovieById(@PathVariable("id") Long id)
            throws RecordNotFoundException {
        Movie entity = service.getMovieById(id);

        return new ResponseEntity<Movie>(entity, new HttpHeaders(), HttpStatus.OK);
    }

    @PostMapping
    public ResponseEntity<Movie> createOrUpdateMovie(Movie movie)
            throws RecordNotFoundException {
        movie.setCreatedAt(new Date());
        Movie updated = service.createOrUpdateMovie(movie);
        return new ResponseEntity<Movie>(updated, new HttpHeaders(), HttpStatus.OK);
    }

    @DeleteMapping("/{id}")
    public HttpStatus deleteMovieById(@PathVariable("id") Long id)
            throws RecordNotFoundException {
        service.deleteMovieById(id);
        return HttpStatus.FORBIDDEN;
    }

}