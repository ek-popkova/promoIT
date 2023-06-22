# :raising_hand: promoIT

PromoIt is a system to promote social campaigns and drive positive change. By onboarding business organizations, non-profit organizations, and social activists on Twitter, PromoIt facilitates collaboration and amplifies the impact of social initiatives. With a focus on simplicity and leveraging the power of social media, PromoIt empowers users to create a better society through effective promotion and engagement. Join us as we revolutionize social campaigns and make a lasting difference.

## :scroll: Table of Contents

- [Technologies Used](#Technologies-used)
- [System architecture](#System-architecture)
- [System Users](#System-Users)
- [User Stories and their implementation](#User-Stories-and-their-implementation)
- [Project Team](#Project-Team)

## :hammer_and_wrench: Technologies Used

### Database:
- MS SQL database hosted in Azure Cloud

### Backend:
- Node.js
  - TypeScript
  - sequelize-typescript
  - twitter-api-v2 for twitter API
- .NET 6
  - C#
  - Entity Framework
- Authentication: Auth0
- Pacman and Swagger to test API
### Frontend:
- Blazor

## :building_construction: System architecture
The system architecture is based on a combination of technologies that communicate with each other using the HTTP protocol. The application follows a multilayer architecture. Client-side requests (Blazor) are routed to the server-side (Node.js and .NET) based on their type. The business logic layer (controllers) on the server-side handles data management and modification. Services and helpers are employed to make targeted adjustments to the controllers in order to achieve the desired data model. Furthermore, database connection classes constitute an additional layer responsible for performing all data operations, encompassing creation, retrieval, updating, and deletion.

<img src="https://github.com/ek-popkova/promoIT-fullstack-web-app/assets/111788752/549bfc87-ac1e-4594-941d-12905744c61d" alt="2023-06-22_16-27-39" style="width: 700px;">


## :speaking_head: System Users

The following user roles are involved in the system:

1. **ProLobby Owner:** Represents the company and manages the system.
2. **Non-Profit Organization Representative:** Creates campaigns on behalf of their organization.
3. **Business Company Representative:** Represents a company that donates products for campaigns.
4. **Social Activists:** Twitter users who actively promote campaigns.

## :framed_picture: User Stories and their implementation

1. As a non-profit organization representative, I want to register in the system to create a campaign.
   - Provide organization details such as name, email, and website link.

<img src="https://github.com/ek-popkova/promoIT-fullstack-web-app/assets/111788752/febf4134-d0a4-4823-96da-876f1ec4f6c7" alt="2023-06-22_17-20-57" style="width: 700px;">

2. As a non-profit organization representative, I want to create a campaign.
   - Specify the campaign landing page URL and a unique campaign hashtag.

<img src="https://github.com/ek-popkova/promoIT-fullstack-web-app/assets/111788752/e8ddda7c-6dee-4e2c-b151-8479660aa571" alt="2023-06-22_17-23-36" style="width: 700px;">

3. As a business company representative, I want to register in the system to donate products.
   - Provide necessary information for registration.

4. As a business company representative, I want to donate goods to selected campaigns.
   - Specify the quantity and value (in dollars) of products donated to each campaign.
  
<img src="https://github.com/ek-popkova/promoIT-fullstack-web-app/assets/111788752/eeb100a8-a4c4-4213-b795-48b2fe0cc164" alt="2023-06-22_16-27-39" style="width: 700px;">

5. As a business company representative, I want to obtain a list of users and products that need to be shipped.
   - Access the relevant data, including product ID and user details.

6. As a business company representative, I want to notify the system when I have sent a product to a user.
   - Mark the transaction as completed in the system.

7. As a social activist, I want to register in the system to earn money and utilize it to purchase products.
   - Provide personal details such as email, address, and phone number.

<img src="https://github.com/ek-popkova/promoIT-fullstack-web-app/assets/111788752/dbb3f188-06cd-4b71-a540-3b67f10e0abb" alt="image" style="width: 700px;">

8. As a social activist, I want to promote campaigns by tweeting about them to earn money.
   
9. As the ProLobby owner, I want to allocate one dollar to social activists for each tweet promoting a campaign, including subsequent retweets.
   - Tweets must contain the campaign page link and hashtag.

10. As a social activist, I want to use my earned money to buy products.

11. As the ProLobby owner, I want the system to automatically post a tweet whenever a social activist uses their points to purchase a product.
   - The tweet should mention the social activist's Twitter handle and the associated business company.

12. As a social activist, I want to view my earning status to keep track of my balance.

14. As a social activist, I want to donate a product to my chosen campaign to further promote it.
   - By earning money, purchasing a product, and donating it, I can increase the campaign's product inventory.
    
<img src="https://github.com/ek-popkova/promoIT-fullstack-web-app/assets/111788752/0fc32849-f4b4-44e0-a336-da88050316f4" alt="image" style="width: 700px;">

14. As the ProLobby owner, I want the system to generate reports on the following:
   a. Campaigns
   b. Users
   c. Tweets

<img src="https://github.com/ek-popkova/promoIT-fullstack-web-app/assets/111788752/42d688d3-63bd-4538-8599-9cd9f450306a" alt="image" style="width: 700px;">

<img src="https://github.com/ek-popkova/promoIT-fullstack-web-app/assets/111788752/55dc8a11-1f13-404d-b2e7-5cbcdf4e04fc" alt="image" style="width: 700px;">


## :globe_with_meridians: Project Team

This project is performed by the following team members:

- Popkova Ekaterina
  - GitHub: [ek-popkova](https://github.com/ek-popkova)
  - LinkedIn: [ekaterina-popkova](https://www.linkedin.com/in/ekaterina-popkova/)

- Daniel Sizov (Lippo)
  - GitHub: [DanSizov](https://github.com/DanSizov)
  - LinkedIn: [daniil-sizov](https://www.linkedin.com/in/daniil-sizov/)

