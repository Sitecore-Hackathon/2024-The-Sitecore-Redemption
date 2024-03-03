[# Hackathon Submission Entry form


## Team name
⟹ The Sitecore Redemption

## Category
⟹ Best Module for XM/XP

## Description
⟹ This module includes a modified Rich Text field (Rich Text - AI Enabled), which contains a "Optimize Content with AI" Function.   Clicking this will call the OpenAI APIs and present you with several options (configurable) that are focused on SEO, readability, and accessibility.  The initial AI instructions can be modifed via a Sitecore item, as well as many of the other OpenAI parameters.

  - Module Purpose
  - Helps authors create SEO-enhanced, readable and accessible content using OpenAI.
    - The module takes the initial content, sends to OpenAI with specific parameters and instructions, and presents the responses in a dialog for the author to choose from.

## Video link
⟹ \[Replace this Video link\](#video-link)


## Installation instructions

1. Use the Sitecore Installation wizard to install the \[SitecorePackages\](SitecorePackages/The.Sitecore.Redemption.Hackathon.2024-1.0.zip)


### Configuration

1. Update OpenAPI token in this item `/sitecore/system/Modules/AI Module/API Configurations/OpenAI Configuration`.  A token will be emailed to judges.

## Usage instructions

1. There is an example page installed with the package at `/sitecore/content/New Sample AI Page`
2. On there, there is a "AI Rich Text" field.  Begin by inserting some general content as you normally would.
3. Click on the "Optimize Content with AI" link on top of the field.
4. OpenAI will present you with options to choose from.  Select one and click Accept.

    - You can update the initial AI instructions by going to this item: `/sitecore/system/Modules/AI Module/Chat Settings/AI Content Optimizer`.
    - For the purposes of this demo, please leave the instructions about the structure of the JSON instact.

!\[AI Field\](docs/images/aifield.png?raw=true "AI Field")

AI Window:

!\[AI Window\](docs/images/aiwindow.png?raw=true "AI Window")

