using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:東方海角(10018001) NPC基本信息:冒險者前輩(11000934) X:64 Y:56
namespace SagaScript.M10018001
{
    public class S11000934 : Event
    {
        public S11000934()
        {
            this.EventID = 11000934;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;

            Say(pc, 11000934, 0, "Hello!$R;" +
                                  "$R people who are just beginning to take risks, $R;" +
                                  "Talking about the team and the corps system! $R;" +
                                  "$R want to listen together? $R;", "Experienced Adventurer");

            selection = Select(pc, "Which aspect of information do you want to listen to?", "", "About the [Team]", "About the [Legion]", "About the [Chat Window]", "About the [Friend Message Window] ", "About [Advertising Directory]", "Not necessary");

            while (selection != 6)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000934, 0, "The team refers to $R;" +
                                             "A companion who fights together or completes a mission. $R;" +
                                             "There are a total of 8 people in the $P team at most. $R;" +
                                             "When $R players are in the same area, $R;" +
                                             "You can share experience points with each other! $R;" +
                                             "$P, but pay attention to the difference in level. $R;" +
                                             "When the level of $R is low, $R;" +
                                             "Even if you defeat a monster with a high level, $R;" +
                                             "It's also possible that you won't get experience points! $R;" +
                                             "The distribution of $P experience points, $R;" +
                                             "It will be different according to your own level! $R;" +
                                             "It is best to find a companion with a similar level, $R;" +
                                             "Or hit a suitable monster to practice fast! $R;" +
                                             "$P as for the method of team formation. $R;" +
                                             "$P please right-click and select the person you want to invite, $R;" +
                                             "Press the [Invite to join the team] option. $R;" +
                                             "$P invites the other party suddenly, it will bother the other party, $R;" +
                                             "Please say hello first, then invite it! $R;" +
                                             "When you want to quit the team, $R;" +
                                             "Right click and select [Exit Team]. $R;" +
                                             "$P when the person who organized the team wants to disband the team, $R;" +
                                             "Select [Disband Team]. $R;", "Experienced Adventurer");
                        break;

                    case 2:
                        Say(pc, 11000934, 0, "You can treat the legion as a semi-permanent team! $R;" +
                                              "$P Legion can register 16 people initially. $R;" +
                                              "$R will increase the reputation of the legion in the future, $R;" +
                                              "You can increase the upper limit of the number of people, $R;" +
                                              "So you can continue to make friends with confidence! $R;" +
                                              "The administrator of the $P legion is in [Acropolis], $R;" +
                                              "When you want to organize a corps, just find the administrator. $R;" +
                                              "$P As for the method of organizing the army, $R;" +
                                              "Let’s ask the legion manager! $R;" +
                                              "$R tells you now, but I can't explain it clearly. $R;", "Experienced Adventurer");
                        break;

                    case 3:
                        Say(pc, 11000934, 0, "The chat window refers to the place where the dialogue is entered. $R;" +
                                              "There is a long window next to $R? $R;" +
                                              "That is the chat window. $R;" +
                                              "There are [O] [P] [R] [W] keys on the left of $P, right? $R;" +
                                              "$P is right! $R;" +
                                              "$R [O] key is a public chat mode, $R;" +
                                              "[P] key is the team chat mode, $R;" +
                                              "[R] key is the legion chat mode, $R;" +
                                              "The [W] key is a private chat mode. $R;" +
                                              "$P public chat is for everyone around, $R;" +
                                              "Team chat is only for players...$R;" +
                                              "The $P private message is used for 1-on-1 chat. $R;" +
                                              "$R just click the [W] button, $R;" +
                                              "Use the private message window, $R;" +
                                              "You can talk to the designated person! $R;", "Experienced Adventurer");
                        break;

                    case 4:
                        Say(pc, 11000934, 0, "Use the [Friends] button in the main window, $R;" +
                                             "Open the friends list! $R;" +
                                             "$P...$R;" +
                                             "Is $P open? $R;" +
                                             "$P now shows that in the friends directory, $R;" +
                                             "[Friends], [Legion], [Team]. $R;" +
                                             "$P[Friends] directory save $R;" +
                                             "Information about registered friends! $R;" +
                                             "$P if it is a registered friend, $R;" +
                                             "You can know if you are online. $R;" +
                                             "If $R is online, $R;" +
                                             "You can also know the location of the other party! $R;" +
                                             "The way to register $P is very simple! $R;" +
                                             "$R wants to be friends, $R;" +
                                             "Just right click, $R;" +
                                             "Select [Add Friends Directory]. $R;" +
                                             "$P If you are the commander of the legion, $R;" +
                                             "You can also use a similar method to invite $R;" +
                                             "Others join your legion! $R;" +
                                             "$P is in the [legion], $R;" +
                                             "You can confirm the messages of members of the legion. $R;" +
                                             "$R is in the [team], $R ;" +
                                             "You can confirm the message of team members. $R;" +
                                             "$P is another little hint! $R;" +
                                             "$R After selecting the name in the friend list, right-click...$R;" +
                                             "$R can use private chat to chat! $R;", "Experienced Adventurer");
                        break;

                    case 5:
                        Say(pc, 11000934, 0, "As for the advertising directory! $R;" +
                                             "In forming a team, $R;" +
                                             "Or say hello to the player on the same servo, $R;" +
                                             "Or when exchanging information, $R;" +
                                             "It's a very convenient function! $R;" +
                                             "$P right click on yourself. $R;" +
                                             "$R saw [Recruitment Advertisement Login] $R;" +
                                             "[Recruiting Advertising Directory], right? $R;" +
                                             "$R first select the directory! $R;" +
                                             "$P after selecting [Recruitment Advertising Directory]. $R;" +
                                             "$P can show the people recruited now in detail! $R;" +
                                             "$R can still see where those people are now? $R;" +
                                             "The name of the person recruited by $P has $R; on the right;" +
                                             "[P] and [W] keys, do you see it? $R;" +
                                             "$P press [P] key, $R;" +
                                             "You can apply to join this person's team. $R;" +
                                             "$R press [W] key, $R;" +
                                             "The private message window will be displayed. $R;" +
                                             "$R but suddenly applied for a team, $R;" +
                                             "The other party will be at a loss, $R;" +
                                             "Tell the other party well before inviting! $R;" +
                                             "$P If you want to recruit members, $R;" +
                                             "Right click on yourself, in the displayed directory $R;" +
                                             "Select [Recruitment Advertisement Login]. $R;", "Experienced Adventurer");
                        break;
                }

                selection = Select(pc, "Which aspect of information do you want to listen to?", "", "About the [Team]", "About the [Legion]", "About the [Chat Window]", "About the [Friend Message Window] ", "About [Advertising Directory]", "Not necessary");
            }

            Say(pc, 11000934, 0, "Now the information about the team and the corps, $R;" +
                                 "Do you understand everything? $R;" +
                                 "If $P is not clear, just ask the people around you? $R;" +
                                 "$R inquiry is also a good way to communicate! $R;" +
                                 "$P, go on! $R;", "Experienced Adventurer");
        }
    }
}
