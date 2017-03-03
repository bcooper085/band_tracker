# _Barks_
#### A Mobile Dog Groomer

## By Brandon Cooper

# Description
##### A mock site to schedule your pets grooming with your favorite groomer.  We come to you.

# Specs

- User can add a Venue
- User can view all available Venues
- User can edit a Venue
- User can delete a Venue
- User can add Band
- User can add Band to a Venue
- User can view all Bands that have played at a specific Venue
- User can add Venue to a Band
- User can view all Venues that have hosted a specific Band

# Icebox
- User can add Genre to a Band

Database Creation:

-In SQLCMD
- CREATE DATABASE band_tracker;
- GO
- USE band_tracker;
- GO
- CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255));
- GO
- CREATE TABLE bands ( id INT IDENTITY(1,1), name VARCHAR(255));
- GO
- CREATE TABLE bands_venues (id INT IDENTITY(1,1), band_id INT, venue_id INT);
- GO

# Known Bugs
No known bugs.

## Legal
Licensed under the MIT License.

<a href="https://github.com/bcooper085/Hair_Salon_Cweek3">Github</a>
Copyright (c) 2017 Copyright Brandon Cooper, All Rights Reserved.
