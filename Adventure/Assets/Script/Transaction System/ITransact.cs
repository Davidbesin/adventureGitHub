using UnityEngine;
using System;

public interface ITransact
{
   public bool HasBought{get;}
   public bool DoYourTransaction();
   public bool AutoTransact{get;}
   public bool AllowBuy{get;}
   public Action toAllowBuy{get;}
   public Action toNotAllowBuy{get;}
} 