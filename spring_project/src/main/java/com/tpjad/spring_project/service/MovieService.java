package com.tpjad.spring_project.service;

import com.tpjad.spring_project.exceptions.RecordNotFoundException;
import com.tpjad.spring_project.model.Movie;
import com.tpjad.spring_project.repository.MovieRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class MovieService {

    @Autowired
    MovieRepository repository;

    public List<Movie> getAllMovies()
    {
        List<Movie> movieList = repository.findAll();

        if(movieList.size() > 0) {
            return movieList;
        } else {
            return new ArrayList<Movie>();
        }
    }

    public Movie getMovieById(Long id) throws RecordNotFoundException
    {
        Optional<Movie> movie = repository.findById(id);

        if(movie.isPresent()) {
            return movie.get();
        } else {
            throw new RecordNotFoundException("No movie record exist for given id");
        }
    }

    public Movie createOrUpdateMovie(Movie entity) throws RecordNotFoundException
    {
        Optional<Movie> movie = repository.findById(entity.id);

        if(movie.isPresent())
        {
            Movie newEntity = new Movie();

            newEntity = repository.save(newEntity);

            return newEntity;
        } else {
            entity = repository.save(entity);

            return entity;
        }
    }

    public void deleteMovieById(Long id) throws RecordNotFoundException
    {
        Optional<Movie> movie = repository.findById(id);

        if(movie.isPresent())
        {
            repository.deleteById(id);
        } else {
            throw new RecordNotFoundException("No movie record exist for given id");
        }
    }
}
