using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CreateCustomAvatarMessage : NetworkMessage
{
    public int AvatarIndex;
}

