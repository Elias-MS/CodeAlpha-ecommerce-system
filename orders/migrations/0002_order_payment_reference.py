# Generated migration for adding payment_reference field

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('orders', '0001_initial'),
    ]

    operations = [
        migrations.AddField(
            model_name='order',
            name='payment_reference',
            field=models.CharField(blank=True, help_text='Transaction/Reference number for online payments', max_length=100),
        ),
    ]
