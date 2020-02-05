package com.tpjad.spring_project.web;

import com.tpjad.spring_project.dto.MovieDto;
import com.tpjad.spring_project.dto.UserDto;
import com.tpjad.spring_project.exceptions.RecordNotFoundException;
import com.tpjad.spring_project.model.Movie;
import com.tpjad.spring_project.service.MovieService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.client.RestTemplate;

import java.util.*;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/movies")
public class MovieController
{
    @Autowired
    MovieService service;

    @GetMapping("/all")
    public ResponseEntity<List<MovieDto>> getAllMovies() {
        List<Movie> list = service.getAllMovies();
        RestTemplate restTemplate = new RestTemplate();
        HttpHeaders headers = new HttpHeaders();
        List<MediaType> mediaList = new ArrayList<MediaType>();
        mediaList.add(MediaType.APPLICATION_JSON);
        mediaList.add(MediaType.TEXT_PLAIN);
        headers.setAccept(mediaList);
        String resourceUrl = "http://localhost:4000/api/Users/";
        HttpEntity<String> request = new HttpEntity<>(headers);

        List<MovieDto> allMovies = list.stream().map(movie -> {
            ResponseEntity<UserDto> response = restTemplate.exchange(resourceUrl+movie.getCreatedBy().toString(), HttpMethod.GET, request, UserDto.class);
            UserDto user = response.getBody();
            return new MovieDto(movie.id, movie.getTitle(), movie.getCreatedAt(), user, movie.getGenre(), movie.getVideoFile());
        }).collect(Collectors.toList());
        return new ResponseEntity<>(allMovies, new HttpHeaders(), HttpStatus.OK);
    }

    @GetMapping("/{id}")
    public ResponseEntity<Movie> getMovieById(@PathVariable("id") Long id)
            throws RecordNotFoundException {
        Movie entity = service.getMovieById(id);

        return new ResponseEntity<Movie>(entity, new HttpHeaders(), HttpStatus.OK);
    }

    @PostMapping
    public ResponseEntity<Movie> createOrUpdateMovie(@RequestBody Movie movie)
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