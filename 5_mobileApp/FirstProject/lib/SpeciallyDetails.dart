import 'package:flutter/material.dart';
import 'data/DoctorService.dart';
import 'data/ConectionString.dart';

class SpeciallyDetails extends StatelessWidget {
  final int specialtyId;
  final String specialtyName;

  SpeciallyDetails({
    super.key,
    required this.specialtyId,
    required this.specialtyName,
  });

  final DoctorService doctorService = DoctorService();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(specialtyName),
        backgroundColor: Colors.deepPurple,
      ),
      body: FutureBuilder<List<dynamic>>(
        // استدعاء دالة الأطباء المخصصة لهذا التخصص مباشرة
        future: doctorService.getDoctorsBySpecialty(specialtyId),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }

          if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          }

          final doctorsList = snapshot.data ?? [];

          if (doctorsList.isEmpty) {
            return const Center(
              child: Text(
                'No Doctors Found for this Specialty',
                style: TextStyle(fontSize: 16, color: Colors.grey),
              ),
            );
          }

          return ListView.builder(
            padding: const EdgeInsets.all(12),
            itemCount: doctorsList.length,
            itemBuilder: (context, index) {
              final doctor = doctorsList[index];

              final String imagePath = doctor['imageUrl']?.toString() ?? '';
              final String imageUrl = imagePath.isEmpty
                  ? ''
                  : Uri.parse(ConectionString.dashboardUrl)
                  .resolve(imagePath)
                  .toString();

              return Card(
                margin: const EdgeInsets.only(bottom: 12),
                elevation: 3,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                child: ListTile(
                  contentPadding: const EdgeInsets.all(10),
                  leading: CircleAvatar(
                    radius: 30,
                    backgroundImage: imageUrl.isNotEmpty
                        ? NetworkImage(imageUrl)
                        : null,
                    child: imageUrl.isEmpty
                        ? const Icon(Icons.person, size: 30)
                        : null,
                  ),
                  title: Text(
                    doctor['name']?.toString() ?? 'Unknown Doctor',
                    style: const TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: 16,
                    ),
                  ),
                  subtitle: Text(
                    doctor['email']?.toString() ?? specialtyName,
                    style: const TextStyle(color: Colors.grey),
                  ),
                  trailing: ElevatedButton(
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.deepPurple,
                    ),
                    onPressed: () {},
                    child: const Text('Book', style: TextStyle(color: Colors.white)),
                  ),
                ),
              );
            },
          );
        },
      ),
    );
  }
}