using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public GameObject Panel;
    public TMP_Text guideText;
    // Start is called before the first frame update

    public void OpenOrClose()
    {
        if(Panel.activeInHierarchy == false)
        {
            Panel.SetActive(true);
        }
        else
        {
            Panel.SetActive(false);
        }
    }
    public void GoToWhen()
    {
        guideText.text = "<b><size=12>When to Contact</size></b>\r\n<size=10>Social services can provide support in various situations. Here are some examples of when you might seek help from social services:\r\n* There are conflicts at home£¬ and you or someone else may be threatened or physically harmed.\r\n* You are experiencing violence or sexual abuse from a family member.\r\n* You are struggling at school.\r\n* Your family cannot afford food or rent.\r\n*The adults you live with are unable to care for you properly£¬ perhaps due to illness or substance abuse problems.\r\n* You are dealing with substance abuse issues or engaging in illegal or dangerous activities.\r\n* Your parents or guardians have refused to let you stay at home.\r\n* You fear being forced into marriage or are restricted from loving someone of your choice.\r\n* You arrived in Sweden alone without an adult to care for you.\r\nThese are just a few typical examples. If you face any challenging situation£¬ feel free to contact social services. We are here to help you. </size>";
    }

    public void GoToHow()
    {
        guideText.text = "<b><size=12> How to Contact </size></b>\r\n<size=10>If you want to contact social services£¬ call your local municipal government and ask to be connected to the social services department or the social emergency service. \r\nIf you only wish to ask questions£¬ seek advice£¬ or get support£¬ you can remain anonymous.\r\nIf you find it difficult to contact social services by yourself£¬ ask someone you trust to help you£¬ like a school staff member£¬ a relative£¬ or a friend¡¯s parent.\r\nFor those in Botkyrka Municipality£¬ you can call:\r\n* Reception for Children Phone: 08-530 622 55\r\nMonday: 8:30 AM - 6:00 PM\r\nTuesday to Thursday: 8:30 AM - 4:00 PM\r\nFriday: 8:30 AM - 2:00 PM (Closed for lunch from 12 PM - 1 PM)\r\n* Social Emergency Service (after office hours) Phone: 020-70 80 03\r\nYou can also contact us online or submit a report via e-service by visiting our website: Visit<link=\\\"https://www.botkyrka.se/stod-trygghet-och-familj/om-socialtjansten/kontakta-socialtjansten\\\">https://www.botkyrka.se/stod-trygghet-och-familj/om-socialtjansten/kontakta-socialtjansten</link></size>";
    }

    public void GoToWhat()
    {
        guideText.text = "<b><size=12> What Help Can I Get?  </size></b>\r\n<size=10>You can receive different types of support from social services. Here are some examples of the help available:\r\n* Family Discussions: You and your family can participate in structured family conversations facilitated by a trained professional. They can help resolve conflicts and create a healthier environment at home.\r\n* Parental Support: Your parents or guardians can receive guidance on how to better care for you.\r\n* Family Law Support: If your parents or guardians are going through a divorce£¬ social services can help determine living arrangements and custody agreements.\r\n* Contact Support: You may be assigned a mentor or support person ¡ª a reliable adult you can talk to and spend time with£¬ whether it¡¯s having coffee£¬ watching a movie£¬ or doing other activities together.\r\n* Temporary Housing: If necessary£¬ you may stay somewhere temporarily. This is often with relatives£¬ family friends£¬ or foster families. In some cases£¬ specialized accommodations are also available. </size>";
    }
}
