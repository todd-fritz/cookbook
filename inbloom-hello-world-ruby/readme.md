inBloom Hello World - Ruby
==========================

Introduction
------------

[inBloom][1] is a non-profit that's trying to "inform and involve each student and teacher with data and tools designed to personalize learning."

This Ruby on Rails program is a quick way to getting a rails app authenticating with inBloom's OAuth2 service and then pulling data from the REST api. If you don't have piles of expience with Rails check out the detailed [inBloom hello world tutorial][2]. If you want to do things the hard way, the interesting bits are in the `app/controller/sessions.rb`, the `config/initializer/omniauth.rb` and the `config/inbloom.yml` configuration file. 



Running the code
----------------
The [inBloom hello world tutorial] discusses these steps in more details.


1. [Create developer's account](https://www.inbloom.org/developer-account-registration)
2. Ingest sample data
3. Register application
4. Get the code `git clone repostuffhere`
5. Copy `config/inbloom.example.yml` to `config/inbloom.yml`
6. Add client id and client secret from step #4 to `config/inbloom.yml`
7. Start the server: `rails server`
8. Browser to localhost:3000



[1]: https://www.inbloom.org/		"inBloom"
[2]: http://insert-link-here		"inBloom hello world tutorial"




